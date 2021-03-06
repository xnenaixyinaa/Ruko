﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xne_MVVM;
using Xne_Utilities;

namespace Ruko
{
    public class ContactProfile : NodeViewModel<Contact, ContactProfileModel>
    {
        public bool IsPrimary
        {
            get => Model.isPrimary;
            set
            {
                if (Model.isPrimary != value)
                {
                    Model.isPrimary = value;
                    OnPropertyChanged();
                    Name.IsPrimary = true;
                }
            }
        }
        public Name Name => Model.name;
        public Address Address
        {
            get => Model.address;
            private set
            {
                if (Model.address != value)
                {
                    Model.address = value;
                    OnPropertyChanged();
                }
            }
        }
        public Email Email
        {
            get => Model.email;
            private set
            {
                if (Model.email != value)
                {
                    Model.email = value;
                    OnPropertyChanged();
                }
            }
        }
        public Phone Phone
        {
            get => Model.phone;
            private set
            {
                if (Model.phone != value)
                {
                    Model.phone = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Address> Addresses { get; } = new ObservableCollection<Address>();
        public ObservableCollection<Email> Emails { get; } = new ObservableCollection<Email>();
        public ObservableCollection<Phone> Phones { get; } = new ObservableCollection<Phone>();

        public ContactProfile(Contact parent, ContactProfileModel model) : base(parent, model)
        {
        }

        public void SetPrimary<T>(T newValue) where T : IContactItem
        {
            IEnumerable<IContactItem> items = null;
            IContactItem currentValue;
            switch (newValue.ContactType)
            {
                case ContactTypes.Address:
                    items = Addresses;
                    currentValue = Address;
                    break;
                case ContactTypes.Email:
                    items = Emails;
                    currentValue = Email;
                    break;
                case ContactTypes.Phone:
                    items = Phones;
                    currentValue = Phone;
                    break;
                default:
                    throw new ArgumentException("Wrong ContactType type");
            }

            currentValue = GetPrimary(items);
        }

        T GetPrimary<T>(IEnumerable<T> items) where T : IContactItem => items.FirstOrDefault(item => item.IsPrimary);
    }

    public class ContactProfileModel
    {
        internal bool isPrimary;

        internal Name name;
        internal Address address;
        internal Email email;
        internal Phone phone;

        internal ObservableCollection<Address> addresses;
        internal ObservableCollection<Email> emails;
        internal ObservableCollection<Phone> phones;

        public ContactProfileModel(Name name, Address address, Email email, Phone phone, IEnumerable<Address> addresses = null, IEnumerable<Email> emails = null, IEnumerable<Phone> phones = null)
        {
            this.name = name;
            this.address = address;
            this.email = email;
            this.phone = phone;

            //this.addresses = addresses == null ? new ObservableCollection<Address>() : new ObservableCollection<Address>(addresses);
            //this.emails = emails == null ? new ObservableCollection<Email>() : new ObservableCollection<Email>(emails);
            //this.phones = phones == null ? new ObservableCollection<Phone>() : new ObservableCollection<Phone>(phones);

            this.addresses = new ObservableCollection<Address>().Conditionialize(addresses);
            this.emails = new ObservableCollection<Email>().Conditionialize(emails);
            this.phones = new ObservableCollection<Phone>().Conditionialize(phones);
        }
    }
}