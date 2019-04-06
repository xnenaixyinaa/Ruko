﻿using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MVVM
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public void UpdateObject() 
            => GetType()
            .GetProperties()
            .Select(property=>property.Name)
            .ToList()
            .ForEach(property => OnPropertyChanged(property));
    }
}