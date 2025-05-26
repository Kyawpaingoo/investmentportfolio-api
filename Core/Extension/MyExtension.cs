using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Core.Extension;

public static class MyExtension
{
     public static string AddCommaToCommaString(this string cmstring)
 {
     return $@",{cmstring},";
 }
 public static string RemoveCommaFromCommaString(this string cmstring)
 {
     return cmstring.Substring(1, cmstring.Length - 2);
 }
 public static string getUniqueCode()
 {
     return Guid.NewGuid().ToString("N");
 }

 public static string getUniqueNumber()
 {
     byte[] buffer = Guid.NewGuid().ToByteArray();
     string uniquecode = BitConverter.ToUInt32(buffer, 12).ToString();
     return uniquecode;
 }



 public static DateTime getUtcTime()
 {
     //DateTime utc = DateTime.UtcNow;
     //return TimeZoneInfo.ConvertTimeFromUtc(utc, TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time"));
     return DateTime.UtcNow;
 }

 public static DateTime getMMTime()
 {
     DateTime utc = DateTime.UtcNow;
     return TimeZoneInfo.ConvertTimeFromUtc(utc, TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time"));

 }

 public static DateTime getLocalTime()
 {
     DateTime utc = DateTime.UtcNow;
     return TimeZoneInfo.ConvertTimeFromUtc(utc, TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time"));

 }
   
 public static string getDateAgo(DateTime dateTime)
 {
     TimeSpan timeSpan = DateTime.Now.Subtract(dateTime);

     if (timeSpan.TotalSeconds < 0)
     {
         return "1 s";
     }

     return timeSpan.TotalSeconds switch
     {
         <= 60 => $"{timeSpan.Seconds} s",

         _ => timeSpan.TotalMinutes switch
         {
             <= 1 => "1 m",
             < 60 => $"{timeSpan.Minutes} m",
             _ => timeSpan.TotalHours switch
             {
                 <= 1 => "1 h",
                 < 24 => $"{timeSpan.Hours} h",
                 _ => timeSpan.TotalDays switch
                 {
                     <= 1 => "1 d",
                     <= 30 => $"{timeSpan.Days} d",

                     <= 60 => "1 m",
                     < 365 => $"{timeSpan.Days / 30} months",

                     <= 365 * 2 => "1 y",
                     _ => $"{timeSpan.Days / 365} yrs"
                 }
             }
         }
     };
 }

 public static byte[] ToByteArray(this System.Drawing.Image image, ImageFormat format)
 {
     using (MemoryStream ms = new MemoryStream())
     {
         //image.Save(ms, format);
         return ms.ToArray();
         //return ms.ToArray();
     }
 }

 public static byte[] ImageToByte(System.Drawing.Image img)
 {
     ImageConverter converter = new ImageConverter();
     return (byte[])converter.ConvertTo(img, typeof(byte[]));
 }
 public static T Convert<T>(object input)
 {
     var converter = TypeDescriptor.GetConverter(typeof(T));
     if (converter.CanConvertFrom(input.GetType()))
     {
         return (T)converter.ConvertFrom(input);
     }
     return default(T);
 }

 public static List<string> SortDaysOfWeek(List<string> days)
 {
     var weekOrder = new List<string>() { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

     return days.OrderBy(day => weekOrder.IndexOf(day)).ToList();

 }

 public static List<T> CloneList<T>(this IEnumerable<T> source) where T : class, new()
 {
     return source.Select(item => item.Clone()).ToList();
 }

 public static T Clone<T>(this T source) where T : class, new()
 {
     var dest = new T();
     var type = typeof(T);
     var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

     foreach (var property in properties)
     {
         if (property.CanWrite)
         {
             property.SetValue(dest, property.GetValue(source));
         }
     }

     return dest;
 }

 public static List<T> DetectChanges<T>(List<T> modifiedEntries, List<T> originalEntries, params Func<T, object>[] propertiesToCompare)
 {
     if (modifiedEntries.Count != originalEntries.Count)
         throw new ArgumentException("The lists must have the same number of elements.");

     return modifiedEntries
         .Zip(originalEntries, (modified, original) => new { Modified = modified, Original = original })
         .Where(pair => propertiesToCompare.Any(prop =>
         {
             var modifiedValue = prop(pair.Modified);
             var originalValue = prop(pair.Original);

             // Handle nullable and value types correctly
             return modifiedValue != null ? !modifiedValue.Equals(originalValue) : originalValue != null;
         }))
         .Select(pair => pair.Modified)
         .ToList();
 }
 public static string getUnixTime()
 {
     DateTime foo = MyExtension.getLocalTime();
     string unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds().ToString();
     return unixTime;
 }

 public static string RemoveDecimalPrecision(decimal? data)
 {
     string formattedClubScore = data?.ToString("N0");
     return formattedClubScore ?? "";
 }
 public static string GetCacheKeyFormattedDateString(DateTime? date)
 {
     return $"{date?.ToString("dd-MMM-yyyy")}";
 }

 public static DateTime getExpireMonth(int month)
 {
     DateTime date = getLocalTime();
     DateTime expiredDate = date.AddMonths(month);
     return expiredDate;
 }

 public static DateTime getExpireDate(int day)
 {
     DateTime date = getLocalTime();
     DateTime expiredDate = date.AddDays(day);
     return expiredDate;
 }

 public static string MaskString(string source, char maskChar)
 {
     if (source.Length <= 3)
         return source; // If string is too short, return as is

     int lengthToMask = source.Length - 3;
     return new string(maskChar, lengthToMask) + source.Substring(lengthToMask);
 }

 public static string FixPhoneNumber(string phone)
 {
     string cleaned = Regex.Replace(phone, @"[^\d+]", "");
     switch (cleaned)
     {
         case var _ when cleaned.StartsWith("09"): 
             return cleaned;
         case var _ when cleaned.StartsWith("+959"): 
             cleaned = cleaned.Substring(3);
             break;
         case var _ when cleaned.StartsWith("959"):
             cleaned = cleaned.Substring(3);
             break;
         case var _ when cleaned.StartsWith("95"):
             cleaned = cleaned.Substring(2);
             break;
         case var _ when cleaned.StartsWith("+9509"): 
             cleaned = cleaned.Substring(4);
             break; 
         case var _ when cleaned.Length == 10 && cleaned.StartsWith("9"): 
            cleaned = "0" + cleaned; 
             return cleaned;
         default: 
             break;

     }
     return "09" + cleaned;
 }

 public static string getDateRemaining(DateTime? date = null)
 {
     if(date == null)
     {
         return "";
     }

     DateTime today = DateTime.Today;
     TimeSpan difference = date.Value.Date - today;

     if (difference.TotalDays <= 0)
         return "0 day";

     if (difference.TotalDays < 7)
         return $"{difference.Days} day{(difference.Days > 1 ? "s" : "")}";

     if (difference.TotalDays < 30)
     {
         int weeks = (int)Math.Ceiling(difference.TotalDays / 7);
         return $"{weeks} week{(weeks > 1 ? "s" : "")}";
     }

     if (difference.TotalDays < 365)
     {
         int months = (int)Math.Ceiling(difference.TotalDays / 30);
         return $"{months} month{(months > 1 ? "s" : "")}";
     }

     int years = (int)Math.Ceiling(difference.TotalDays / 365);
     return $"{years} year{(years > 1 ? "s" : "")}";
 }

 public static string ReplaceMiddleWithAsterisks(string input)
 {
     if (input.Length < 6)
     {
         throw new ArgumentException("Input string must be at least 6 characters long.");
     }

     return input.Substring(0, 2) + "****" + input.Substring(6);
 }
}