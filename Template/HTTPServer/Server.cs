using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace HTTPServer
{
    class Server
    {
        Socket serverSocket;

        public Server(int portNumber, string redirectionMatrixPath)
        {
            this.LoadRedirectionRules(redirectionMatrixPath);
            //TODO: initialize this.serverSocket
            this.serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, portNumber);
            this.serverSocket.Bind(endpoint);
        }

        public void StartServer()
        {
            throw new NotImplementedException();
            // TODO: Listen to connections, with large backlog.

            // TODO: Accept connections in while loop and start a thread for each connection on function "Handle Connection"
            while (true)
            {
                //TODO: accept connections and start thread for each accepted connection.

            }
        }

        public void HandleConnection(object obj)
        {
            Socket clientSocket = (Socket)obj;
            clientSocket.ReceiveTimeout = 0; //indicates an infinite time-out period.
            // TODO: receive requests in while true until remote client closes the socket.
            while (true)
            {
                try
                {
                    throw new NotImplementedException();
                    // TODO: Receive request

                    // TODO: break the while loop if receivedLen==0

                    // TODO: Create a Request object using received request string

                    // TODO: Call HandleRequest Method that returns the response

                    // TODO: Send Response back to client

                }
                catch (Exception ex)
                {
                    Logger.LogException(ex);
                }
            }

            clientSocket.Close();
        }

        Response HandleRequest(Request request)
        {
            throw new NotImplementedException();
            string content;
            try
            {
                //TODO: check for bad request 



                //TODO: map the relativeURI in request to get the physical path of the resource.

                //TODO: check for redirect


                //TODO: check file exists


                //TODO: read the physical file



                // Create OK response


            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                // TODO: in case of exception, return Internal Server Error. 
            }
        }


        /// <summary>
        /// Gets the redirection page path IF exist.
        /// </summary>
        /// <param name="relativePath">The relative path.</param>
        /// <returns>The redirection path, empty string otherwise</returns>
        private string GetRedirectionPagePathIFExist(string relativePath)
        {
            if (Configuration.RedirectionRules.ContainsKey(relativePath))
            {
                return Configuration.RedirectionRules[relativePath];
            }

            return string.Empty;
        }

        private string LoadDefaultPage(string defaultPageName)
        {
            string filePath = Path.Combine(Configuration.RootPath, defaultPageName);
            if (!File.Exists(filePath))
            {
                Logger.LogException(new Exception("Default Page " + defaultPageName + " doesn't exist"));
                return string.Empty;
            }
            StreamReader reader = new StreamReader(filePath);
            string file = reader.ReadToEnd();
            reader.Close();
            return file;

        }

        /// <summary>
        /// Loads the redirection rules from the configuration file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        private void LoadRedirectionRules(string filePath)
        {
            try
            {
                StreamReader reader = new StreamReader(filePath);


                Configuration.RedirectionRules = new Dictionary<string, string>();

                while (!reader.EndOfStream)
                {
                    string temp = reader.ReadLine();
                    string[] result = temp.Split(',');
                    Configuration.RedirectionRules.Add(result[0], result[1]);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);

                Environment.Exit(1);
            }
        }


    }
}
