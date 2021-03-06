using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;

namespace DancingGoat.Models.Widgets
{
    /// <summary>
    /// Hero image widget properties.
    /// </summary>
    public class StarkImageWidgetProperties : IWidgetProperties
    {
        /// <summary>
        /// Media library name.
        /// </summary>
        public const string MEDIA_LIBRARY_NAME = "Graphics";


        /// <summary>
        /// Guid of background image.
        /// </summary>
        [EditingComponent(MediaFilesSelector.IDENTIFIER, Label = "{$DancingGoatMVC.Widget.StarkImage.BackgroundImage$}", Order = 1)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.LibraryName), MEDIA_LIBRARY_NAME)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.MaxFilesLimit), 1)]
        [EditingComponentProperty(nameof(MediaFilesSelectorProperties.AllowedExtensions), ".gif;.png;.jpg;.jpeg")]
        [Required]
        public IEnumerable<MediaFilesSelectorItem> Image { get; set; } = Enumerable.Empty<MediaFilesSelectorItem>();


        /// <summary>
        /// Text to be displayed.
        /// </summary>
        public string Text { get; set; }


        /// <summary>
        /// Button text.
        /// </summary>
        public string ButtonText { get; set; }


        /// <summary>
        /// Target of button link.
        /// </summary>
        [EditingComponent(TextInputComponent.IDENTIFIER, Label = "{$DancingGoatMVC.Widget.StarkImage.ButtonTarget$}", Order = 2)]
        public string ButtonTarget { get; set; }


        /// <summary>
        /// Theme of the widget.
        /// </summary>
        [EditingComponent(DropDownComponent.IDENTIFIER, Label = "{$DancingGoatMVC.Widget.StarkImage.ColorScheme$}", Order = 3)]
        [EditingComponentProperty(nameof(DropDownProperties.DataSource), "light;{$DancingGoatMVC.Widget.StarkImage.ColorScheme.Light$}\r\ndark;{$DancingGoatMVC.Widget.StarkImage.ColorScheme.Dark$}")]
        public string Theme { get; set; } = "dark";
    }
}