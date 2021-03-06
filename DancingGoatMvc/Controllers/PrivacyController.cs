using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

using CMS.Base;
using CMS.ContactManagement;
using CMS.Core;
using CMS.DataProtection;
using CMS.Helpers;
using CMS.Membership;
using CMS.Scheduler;

using Kentico.Content.Web.Mvc;
using Kentico.Content.Web.Mvc.Routing;
using Kentico.Web.Mvc;

using DancingGoat.Controllers;
using DancingGoat.Helpers.Generator;
using DancingGoat.Models.Privacy;

[assembly: RegisterPageRoute("DancingGoatMvc.Privacy", typeof(PrivacyController))]

namespace DancingGoat.Controllers
{
    public class PrivacyController : Controller
    {
        private readonly IConsentAgreementService consentAgreementService;
        private readonly ICurrentCookieLevelProvider cookieLevelProvider;
        private readonly ICurrentUserContactProvider currentContactProvider;
        //private readonly ICurrentUserContactUsProvider currentContactUsProvider;
        private readonly IWebFarmService webFarmService;
        private readonly ISiteService siteService;
        private readonly IConsentInfoProvider consentInfoProvider;
        private readonly ITaskInfoProvider taskInfoProvider;
        private ContactInfo currentContact;
        private ContactInfo currentContactUs;

        private const string SUCCESS_RESULT = "success";
        private const string ERROR_RESULT = "error";


        private ContactInfo CurrentContact
        {
            get
            {
                if (currentContact == null)
                {
                    // Try to get contact from cookie
                    currentContact = ContactManagementContext.CurrentContact;

                    // If contact is not found, get the contact for current user regardless of the cookie level set
                    if (currentContact == null)
                    {
                        currentContact = currentContactProvider.GetContactForCurrentUser(MembershipContext.AuthenticatedUser);
                    }
                }

                return currentContact;
            }
        }

        //ADD to contactUs 
        private ContactInfo CurrentContactUs
        {
            get
            {
                if (currentContactUs == null)
                {
                    // Try to get contactUs from cookie
                    currentContactUs = ContactManagementContext.CurrentContact;

                    // If contactUs is not found, get the contactUs for current user regardless of the cookie level set
                    if (currentContactUs == null)
                    {
                        currentContactUs = currentContactProvider.GetContactForCurrentUser(MembershipContext.AuthenticatedUser);
                    }
                }

                return currentContactUs;
            }
        }


        public PrivacyController(IConsentAgreementService consentAgreementService, ICurrentCookieLevelProvider cookieLevelProvider,
            ICurrentUserContactProvider currentContactProvider, ICurrentUserContactProvider currentContactUsProvider,  IWebFarmService webFarmService, ISiteService siteService, IConsentInfoProvider consentInfoProvider,
            ITaskInfoProvider taskInfoProvider)
        {
            this.consentAgreementService = consentAgreementService;
            this.cookieLevelProvider = cookieLevelProvider;
            this.currentContactProvider = currentContactProvider;
            this.currentContactProvider = currentContactUsProvider;
            this.webFarmService = webFarmService;
            this.siteService = siteService;
            this.consentInfoProvider = consentInfoProvider;
            this.taskInfoProvider = taskInfoProvider;
        }

        
        public ActionResult Index()
        {
            var model = new PrivacyViewModel();

            if (!IsDemoEnabled())
            {
                model.DemoDisabled = true;
            }
            else if (CurrentContact != null)
            {
                model.Constents = GetAgreedConsentsForCurrentContact();
            }
            else if (CurrentContactUs != null)
            {
                model.Constents = GetAgreedConsentsForCurrentContactUs();
            }

            model.ShowSavedMessage = TempData[SUCCESS_RESULT] != null;
            model.ShowErrorMessage = TempData[ERROR_RESULT] != null;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Privacy/Revoke")]
        public ActionResult Revoke(string consentName)
        {
            var consentToRevoke = consentInfoProvider.Get(consentName);

            if (consentToRevoke != null && CurrentContact != null)
            {
                consentAgreementService.Revoke(CurrentContact, consentToRevoke);

                if (consentName == TrackingConsentGenerator.CONSENT_NAME)
                {
                    cookieLevelProvider.SetCurrentCookieLevel(cookieLevelProvider.GetDefaultCookieLevel());
                    ExecuteRevokeTrackingConsentTask(siteService.CurrentSite, CurrentContact);
                }

                TempData[SUCCESS_RESULT] = true;
            }
            else
            {
                TempData[ERROR_RESULT] = true;
            }
            if (consentToRevoke != null && CurrentContactUs != null)
            {
                consentAgreementService.Revoke(CurrentContactUs, consentToRevoke);

                if (consentName == TrackingConsentGenerator.CONSENT_NAME)
                {
                    cookieLevelProvider.SetCurrentCookieLevel(cookieLevelProvider.GetDefaultCookieLevel());
                    ExecuteRevokeTrackingConsentTask(siteService.CurrentSite, CurrentContactUs);
                }

                TempData[SUCCESS_RESULT] = true;
            }
            else
            {
                TempData[ERROR_RESULT] = true;
            }

            return Redirect(Url.Kentico().PageUrl(ContentItemIdentifiers.PRIVACY));
        }


        private IEnumerable<ConsentViewModel> GetAgreedConsentsForCurrentContact()
        {
            return consentAgreementService.GetAgreedConsents(CurrentContact)
                .Select(consent => new ConsentViewModel
                {
                    Name = consent.Name,
                    Title = consent.DisplayName,
                    Text = consent.GetConsentText(Thread.CurrentThread.CurrentCulture.Name).ShortText
                });
        }

        private IEnumerable<ConsentViewModel> GetAgreedConsentsForCurrentContactUs()
        {
            return consentAgreementService.GetAgreedConsents(CurrentContactUs)
                .Select(consent => new ConsentViewModel
                {
                    Name = consent.Name,
                    Title = consent.DisplayName,
                    Text = consent.GetConsentText(Thread.CurrentThread.CurrentCulture.Name).ShortText
                });
        }


        private bool IsDemoEnabled()
        {
            return consentInfoProvider.Get(TrackingConsentGenerator.CONSENT_NAME) != null;
        }


        private void ExecuteRevokeTrackingConsentTask(ISiteInfo site, ContactInfo contact /*ContactUsInfo contactUs*/)
        {
            const string TASK_NAME_PREFIX = "DataProtectionSampleRevokeTrackingConsentTask";
            string taskName = $"{TASK_NAME_PREFIX}_{contact.ContactID}";
            //taskName = $"{TASK_NAME_PREFIX}_{contactUs.ContactUsID}";

            // Do not create same task if already scheduled
            var scheduledTask = taskInfoProvider.Get(taskName, site.SiteID);
            if (scheduledTask != null)
            {
                return;
            }

            var currentServerName = WebFarmHelper.ServerName;
            var taskServerName = webFarmService.GetEnabledServerNames().First(serverName => !currentServerName.Equals(serverName, StringComparison.Ordinal));

            TaskInterval interval = new TaskInterval
            {
                StartTime = DateTime.Now,
                Period = SchedulingHelper.PERIOD_ONCE
            };

            var task = new TaskInfo
            {
                TaskAssemblyName = "CMS.UIControls",
                TaskClass = "Samples.DancingGoat.RevokeTrackingConsentTask",
                TaskEnabled = true,
                TaskLastResult = string.Empty,
                TaskData = contact.ContactID.ToString(),
                //TaskData = contactUs.ContactUsID.ToString(),
                TaskDisplayName = "Data protection sample - Revoke tracking consent",
                TaskName = taskName,
                TaskType = ScheduledTaskTypeEnum.System,
                TaskInterval = SchedulingHelper.EncodeInterval(interval),
                TaskNextRunTime = SchedulingHelper.GetFirstRunTime(interval),
                TaskDeleteAfterLastRun = true,
                TaskRunInSeparateThread = true,
                TaskSiteID = site.SiteID,
                TaskServerName = taskServerName,
                TaskAvailability = TaskAvailabilityEnum.Administration
            };

            taskInfoProvider.Set(task);
        }
    }
}
