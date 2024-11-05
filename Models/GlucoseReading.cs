using System;


namespace BloodGlucoseTracker.Models
{
    
    public enum MealType
    {
        None,
        Breakfast,
        Lunch,
        Dinner,
        Snack
    }

    public enum StressLevel
    {
        Low,
        Medium,
        High
    }
    public class GlucoseReading
    {
        public string? Date { get; set; }
        
        public string? Time { get; set; }
        public double? FastingGlucose { get; set; }
        public double? PreMealGlucose { get; set; }
        public double? PostMeal1Hr { get; set; }
        public double? PostMeal2Hr { get; set; }
        public MealType? Meal { get; set; }
        public bool? ExerciseDone { get; set; }
        public int? SleepHours { get; set; }
        public StressLevel? Stress { get; set; }
        public string Notes { get; set; }
        
        public GlucoseReading()
        {
            Date = DateTime.Now.ToString("dd/MM/yyyy");
            Time = DateTime.Now.ToString("HH:mm");
            FastingGlucose = null;
            PreMealGlucose = null;
            PostMeal1Hr = null;
            PostMeal2Hr = null;
            Meal = MealType.None;
            ExerciseDone = false;
            SleepHours = 0;
            Stress = StressLevel.Medium;
            Notes = string.Empty;
        }
    }
}