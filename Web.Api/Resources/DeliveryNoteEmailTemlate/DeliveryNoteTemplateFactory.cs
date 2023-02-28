using Web.Api.Resources.BaseEmailTemplate;
using Web.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Resources.DeliveryNoteEmailTemlate
{
    public class DeliveryNoteTemplateFactory : BaseEmailTemplateFactory
    {
        private readonly IEnumerable<DeliveryTemplateNoteItemsViewModel> _items;
        public DeliveryNoteTemplateFactory(IEnumerable<DeliveryTemplateNoteItemsViewModel> items)
        {
            _items = items;
        }

        public override string GetContent()
        {
            var itemsRows = _items.Aggregate(string.Empty,
                                            (acum, item) => acum + string.Format(CodesStrings.DeliveryNoteTemplateItemRow, item.ProductDescription, item.Quantity) + Environment.NewLine);

            var body = File.ReadAllText(CodesStrings.DeliveryNoteEmailTemplateFile);
            body = body.Replace(CodesStrings.DeliveryNoteTemplateItemsToken, itemsRows);
            var template = new GeneralEmailTemplateFactory(new BaseEmailTemplateViewModel
            {
                Title = string.Empty,
                Body = body
            });

            return template.GetContent();
        }
    }
}
