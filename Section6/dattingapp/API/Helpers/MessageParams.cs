namespace Section6.dattingapp.API.Helpers
{
    public class MessageParams :PaginationParams
    {
        public string  Username { get; set; }
        public string  Container { get; set; }="Unread";
    }
}