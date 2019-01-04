using System;

namespace ORM
{
    [AttributeUsage(
   AttributeTargets.Class |
   AttributeTargets.Constructor |
   AttributeTargets.Field |
   AttributeTargets.Method |
   AttributeTargets.Property,
   AllowMultiple = true)]
    public class MetaData : System.Attribute 
    {

        public MetaData(bool isIdentity=false,bool isPrimary=false,bool notDbField=false,bool viewField=false)
        {
            this.isIdentity = isIdentity;
            this.isPrimary = isPrimary;
            this.notDbField = notDbField;
            this.viewField = viewField;
        }
        private bool isIdentity;
        private bool isPrimary;
        private bool notDbField;
        private bool viewField;
        /// <summary>
        /// مشخص کننده فیلد با مقدار دهی خودکار
        /// </summary>
        public bool IsIdentity
        {
            get
            {
                return isIdentity;
            }
        }
        /// <summary>
        /// مشخص کننده کلید اصلی
        /// </summary>
        public bool IsPrimary
        {
            get
            {
                return isPrimary;
            }
        }
        /// <summary>
        /// پراپرتی با این ویژگی فیلد در نظر گرفته نمیشود
        /// </summary>
        public bool NotDbField
        {
            get
            {
                return notDbField;
            }
        }
        /// <summary>
        /// این فیلد جزو فیلدهای اصلی نبوده ولی موقع دریافت اطلاعات از طریق جوین جداول مقدار دهی میشود
        /// </summary>
        public bool ViewField
        {
            get
            {
                return viewField;
            }
        }
    }
}
