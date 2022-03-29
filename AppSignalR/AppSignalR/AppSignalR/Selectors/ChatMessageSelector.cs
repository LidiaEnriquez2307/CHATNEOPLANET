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
            var list = (CollectionView)container;

            if (item is Mensaje)
            {
                return (DataTemplate)list.Resources["LocalSimpleText"];
            }
            return null;
        }
    }
}
