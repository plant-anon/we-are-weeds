using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FirstApp.Util {
	[HtmlTargetElement("recaptcha", TagStructure = TagStructure.WithoutEndTag)]
	public class RecaptchaTagHelper : TagHelper {
		public override void Process(TagHelperContext context, TagHelperOutput output) {
			output.TagName = "div";
			output.Attributes.SetAttribute("class", "g-recaptcha");
			output.Attributes.SetAttribute("data-sitekey", AppConfig.RecaptchaSiteKey);
		}
	}
}
