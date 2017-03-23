using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WpfMvvmTest
{
    public class Observable : INotifyPropertyChanged
    {
        protected void DoNotifyChanged(string propertyName)
        {
            var p = PropertyChanged;
            if (p == null) return;
            p(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
