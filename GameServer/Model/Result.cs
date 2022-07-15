namespace GameServer.Model
{
    internal class Result
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TotalCount { get; set; }
        public int WinCount { get; set; }

        public Result(int id, int userId, int totalCount, int winCount)
        {
            this.Id = id;
            this.UserId = userId;
            this.TotalCount = totalCount;
            this.WinCount = winCount;
        }
    }
}
