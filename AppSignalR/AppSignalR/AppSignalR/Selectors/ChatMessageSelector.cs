using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using AppSignalR.Models;

namespace AppSignalR.Selectors
{
    public class ChatMessageSelector : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var mes = (Mensaje)item;
            var list = (CollectionView)container;

            if (mes.remitente)
            {
                return (DataTemplate)list.Resources["UserText"];
            }
            else
            {
                return (DataTemplate)list.Resources["OtherText"];
            }
        }
    }
}
