namespace OnlineJobPortal.Models
{
    public class JobsModel
    {
        public int job_id { get; set; }
        public int company_id { get; set; }
        public string job_title { get; set; }
        public string job_description { get; set;}
        public string job_type { get; set;}
        public string job_location { get; set;}
        public int job_salary { get; set; }
        public string job_posted_date { get; set; }
        public string job_deadline { get; set;}
    }
}
