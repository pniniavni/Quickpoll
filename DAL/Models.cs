// ה-namespace הוא "שם המשפחה" של המחלקות. הוא מאפשר לפרויקט ה-API למצוא אותן.
namespace DAL;


// ============================================================
// כל class כאן הופך לטבלה במסד הנתונים.
// כל property (שורה עם get; set;) הופך לעמודה בטבלה.
// שדה בשם Id הופך אוטומטית למפתח ראשי עם מספור אוטומטי.
// ============================================================




// ---------- טבלה 1: חברות עסקיות ----------
public class Company
{
    public int Id { get; set; }          // קוד חברה (מפתח ראשי)
    public string Name { get; set; }     // שם חברה


    // קשר: חברה אחת ← הרבה משתמשים.
    // הרשימה הזאת לא נשמרת כעמודה, היא רק מאפשרת לגשת למשתמשים של החברה מהקוד.
    public List<User> Users { get; set; }
}




// ---------- טבלה 2: קופונים ----------
public class Coupon
{
    public int Id { get; set; }                  // קוד קופון (מפתח ראשי)
    public string Description { get; set; }      // תיאור
    public string Image { get; set; }            // תמונה (שם הקובץ או הנתיב שלו)
    public string FileToDownload { get; set; }   // קובץ להורדה (שם הקובץ או הנתיב שלו)


    // קשר: קופון אחד ← יכול להיות מחובר להרבה סקרים
    public List<Survey> Surveys { get; set; }
}




// ---------- טבלה 4: קטגוריות ----------
public class Category
{
    public int Id { get; set; }          // קוד קטגוריה (מפתח ראשי)
    public string Name { get; set; }     // שם קטגוריה


    // קשר: קטגוריה אחת ← הרבה סקרים
    public List<Survey> Surveys { get; set; }
}




// ---------- טבלה 5: משתמשים ----------
public class User
{
    public int Id { get; set; }          // קוד משתמש (מפתח ראשי)
    public string Mail { get; set; }     // מייל
    public string Password { get; set; } // סיסמה


    // מפתח זר: איזו חברה שייך המשתמש.
    // CompanyId הוא העמודה שנשמרת בטבלה, והמספר שבה הוא ה-Id של החברה.
    public int CompanyId { get; set; }
    // האובייקט עצמו, כדי שאפשר יהיה לכתוב user.Company.Name בקוד
    public Company Company { get; set; }


    // קשר: משתמש אחד ← מעלה הרבה סקרים
    public List<Survey> Surveys { get; set; }
    // קשר: משתמש אחד ← עונה על הרבה תשובות
    public List<UserAnswer> UserAnswers { get; set; }
}




// ---------- טבלה 3: סקרים ----------
public class Survey
{
    public int Id { get; set; }          // קוד סקר (מפתח ראשי)
    public string Name { get; set; }     // שם הסקר
    public DateTime Date { get; set; }   // תאריך


    // מפתח זר ← קופונים (איזה קופון מחובר לסקר)
    public int CouponId { get; set; }
    public Coupon Coupon { get; set; }


    // מפתח זר ← משתמשים (מי העלה את הסקר)
    public int UserId { get; set; }
    public User User { get; set; }


    // מפתח זר ← קטגוריות (באיזו קטגוריה הסקר)
    public int CategoryId { get; set; }
    public Category Category { get; set; }


    // קשר: סקר אחד ← הרבה שאלות
    public List<Question> Questions { get; set; }
    // קשר: סקר אחד ← הרבה רשומות בטבלת "תשובות של הסקר"
    public List<SurveyAnswer> SurveyAnswers { get; set; }
}




// ---------- טבלה 6: שאלות של סקר ----------
public class Question
{
    public int Id { get; set; }          // קוד שאלה (מפתח ראשי)
    public string Text { get; set; }     // טקסט השאלה


    // מפתח זר ← סקרים (לאיזה סקר השאלה שייכת)
    public int SurveyId { get; set; }
    public Survey Survey { get; set; }


    // קשר: שאלה אחת ← עד 4 תשובות אפשריות
    public List<Option> Options { get; set; }
}




// ---------- טבלה 7: תשובות אפשריות ----------
public class Option
{
    public int Id { get; set; }              // קוד (מפתח ראשי)
    public int AnswerNumber { get; set; }    // מספר תשובה (1 עד 4)
    public string Text { get; set; }         // טקסט התשובה


    // מפתח זר ← שאלות (לאיזו שאלה התשובה האפשרית שייכת)
    public int QuestionId { get; set; }
    public Question Question { get; set; }


    // קשר: אפשרות אחת ← יכולים לבחור בה הרבה משתמשים
    public List<UserAnswer> UserAnswers { get; set; }
}




// ---------- טבלה 8: תשובות של הסקר ----------
public class SurveyAnswer
{
    public int Id { get; set; }          // קוד (מפתח ראשי)


    // מפתח זר ← סקרים
    public int SurveyId { get; set; }
    public Survey Survey { get; set; }
}




// ---------- טבלה 9: תשובות משתמש ----------
// זו טבלת קישור: היא רושמת איזה משתמש בחר איזו אפשרות.
public class UserAnswer
{
    public int Id { get; set; }          // קוד תשובת משתמש (מפתח ראשי)


    // מפתח זר ← משתמשים (מי ענה)
    public int UserId { get; set; }
    public User User { get; set; }


    // מפתח זר ← תשובות אפשריות (איזו אפשרות הוא בחר)
    public int OptionId { get; set; }
    public Option Option { get; set; }
}