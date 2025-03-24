namespace ProjectComp1640.Model
{
    public class ClassStudent
    {
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}
