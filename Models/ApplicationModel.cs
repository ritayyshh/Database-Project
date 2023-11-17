namespace OnlineJobPortal.Models
{
    public class ApplicationModel
    {
        public int application_id {  get; set; }
        public int job_id { get; set; }
        public int user_id { get; set; }
        public string application_status { get; set; }
        public string application_date { get; set; }
        public string suitable_interview_time { get; set; }
    }
}
