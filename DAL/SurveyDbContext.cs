// נותן לנו את DbContext ו-DbSet (מגיע מהחבילה Microsoft.EntityFrameworkCore)
using Microsoft.EntityFrameworkCore;


namespace DAL;


// המחלקה הזו מייצגת את מסד הנתונים כולו.
// היא יורשת מ-DbContext (": DbContext"), וזה מה שהמורה ביקשה.
public class SurveyDbContext : DbContext
{
    // ה-constructor מקבל את הגדרות החיבור למסד (ה-Connection String).
    // ההגדרות האלה מגיעות מ-Program.cs, בשלב הבא.
    public SurveyDbContext(DbContextOptions<SurveyDbContext> options) : base(options)
    {
    }


    // כל DbSet הוא אוסף שמייצג טבלה אחת במסד.
    // שם ה-DbSet (ברבים) יהיה שם הטבלה במסד הנתונים.
    public DbSet<Company> Companies { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<Survey> Surveys { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
    public DbSet<UserAnswer> UserAnswers { get; set; }


    // כאן אפשר לשנות את ברירות המחדל של הקשרים בין הטבלאות.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // ברירת המחדל היא "מחיקה מדורגת" (Cascade): כשמוחקים משתמש, נמחק גם כל מה שמחובר אליו.
        // SQL Server לא מרשה שני מסלולי מחיקה מדורגת לאותה טבלה,
        // וכאן יש כאלה (משתמש ← תשובת משתמש ישירות, וגם דרך סקר ← שאלה ← אפשרות).
        // Restrict אומר "אל תמחק אוטומטית", וכך נמנעת שגיאה ב-Update-Database.


        modelBuilder.Entity<UserAnswer>()
            .HasOne(ua => ua.User)                // לכל תשובת משתמש יש משתמש אחד
            .WithMany(u => u.UserAnswers)         // ולמשתמש יש הרבה תשובות
            .HasForeignKey(ua => ua.UserId)       // המפתח הזר הוא UserId
            .OnDelete(DeleteBehavior.Restrict);   // בלי מחיקה מדורגת


        modelBuilder.Entity<Survey>()
            .HasOne(s => s.User)                  // לכל סקר יש משתמש אחד שהעלה אותו
            .WithMany(u => u.Surveys)             // ולמשתמש יש הרבה סקרים
            .HasForeignKey(s => s.UserId)         // המפתח הזר הוא UserId
            .OnDelete(DeleteBehavior.Restrict);   // בלי מחיקה מדורגת
    }
}