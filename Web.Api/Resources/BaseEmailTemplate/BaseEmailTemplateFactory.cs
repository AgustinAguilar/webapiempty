using Web.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Resources.BaseEmailTemplate
{
    public abstract class BaseEmailTemplateFactory
    {
        public abstract string GetContent();

    }
    public class GeneralEmailTemplateFactory : BaseEmailTemplateFactory
    {
        private readonly BaseEmailTemplateViewModel _data;
        public GeneralEmailTemplateFactory(BaseEmailTemplateViewModel data)
        {
            _data = data;
        }

        public override string GetContent()
        {
            var template = File.ReadAllText(CodesStrings.BaseEmailTemplateFile);
            template = template.Replace(CodesStrings.BaseEmailTemplateTitleToken, _data.Title);
            template = template.Replace(CodesStrings.BaseEmailTemplateBodyToken, _data.Body);
            return template;
        }
    }
}
