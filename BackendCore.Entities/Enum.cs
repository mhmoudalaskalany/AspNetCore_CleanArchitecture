using System;
using BackendCore.Entities;

namespace BackendCore.Entities
{
    public enum Gender
    {
        [Values("Male", "ذكر", "")]
        Male = 1,
        [Values("Female", "أنثى", "")]
        Female,
        [Values("Both", "الكل", "")]
        Both
    }

    public enum UserType
    {
        [Values("Admin", "مدير", "")]
        Admin = 1,
        [Values("User", "مستخدم", "")]
        User = 2
    }

    internal class Values : Attribute
    {
        public string NameEn;
        public string NameAr;
        public string Code;
        public Values(string nameEn, string nameAr, string code)
        {
            NameAr = nameAr;
            NameEn = nameEn;
            Code = code;
        }
    }
}
public static class Extensions
{
    public static EnumResult GetName(this Enum e)
    {
        var type = e.GetType();

        var memInfo = type.GetMember(e.ToString());

        if (memInfo.Length > 0)
        {
            var attrs = memInfo[0].GetCustomAttributes(typeof(Values), false);
            if (attrs.Length > 0)
            {
                var attributes = (Values)attrs[0];
                return new EnumResult
                {
                    Id = Convert.ToInt32(e),
                    NameEn = attributes.NameEn,
                    NameAr = attributes.NameAr,
                    Code = attributes.Code
                };
            }
        }

        throw new ArgumentException("Name " + e + " has no Name defined!");
    }
}

public class EnumResult
{
    public int Id { get; set; }
    public string NameEn { get; set; }
    public string NameAr { get; set; }
    public string Code { get; set; }
}

