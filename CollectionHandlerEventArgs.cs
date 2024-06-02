using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab13
{
    //Класс для передачи информации о событии изменения коллекции
    public class CollectionHandlerEventArgs : EventArgs
    {
        //свойство с информацией о типе изменений в коллекции
        public string ChangeType { get; set; }

        //свойство для ссылки на объект, с которым связаны изменения
        public object ChangedItem { get; set; }

        public string CollectionName { get; set; }


        public CollectionHandlerEventArgs(string collectionName, string changeType, object changedItem)
        {

            ChangeType = changeType;
            ChangedItem = changedItem;
            CollectionName = collectionName;
        }
    }
}
