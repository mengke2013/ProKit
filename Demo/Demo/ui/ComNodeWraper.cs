using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocky.Core.Opc.Ua;
namespace Demo.ui
{
    class ComNodeWraper : INotifyPropertyChanged
    {
        private OpcNode mOpcNode;
        private string mValue;
        public event PropertyChangedEventHandler PropertyChanged;

        public ComNodeWraper(OpcNode opcNode) {
            mOpcNode = opcNode;
            opcNode.Notification += new NodeValueUpdateEventHandler(NodeValueUpdate);
        }

        public string Value
        {
            get
            {
                return mValue;
            }
            set
            {
                mValue = value;
                Notify("Value");
            }
        }

        void Notify(string propName)
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private void NodeValueUpdate(OpcNode opcNode, Object newValue)
        {
            Value = newValue.ToString();
        }
    }
}
