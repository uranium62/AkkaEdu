﻿namespace GroupRouteAkka.Messages
{
    class ProcessFileMessage
    {
        public string FileName { get; private set; }

        public ProcessFileMessage(string fileName)
        {
            FileName = fileName;
        }
    }
}
