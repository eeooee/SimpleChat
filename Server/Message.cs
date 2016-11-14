namespace Server
{
    class Message
    {
        private string _userName;
        public string userName
        {
            get
            {
                return _userName;
            }
        }

        private string _message;

        public string message
        {
            get
            {
                return _message;
            }
        }
        
        public User userReference;
        public Message(User user, string input)
        {
            string name = user.Name;
            userReference = user; 
            this._userName = name;
            this._message = input;
        }

        public override string ToString()
        {
            string chatline = userName + ": " + message;
            return chatline;
        }
    }
}