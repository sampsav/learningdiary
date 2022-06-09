namespace LearningDiary
{
    class LearningDiaryItem
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public LearningDiaryItem() { }
        public LearningDiaryItem(int id, string title, string description)
        {
            this.id = id;
            this.Title = title;
            this.Description = description;


        }
    }
}
