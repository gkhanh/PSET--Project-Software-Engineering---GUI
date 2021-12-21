using Castle.Core;
using MySqlConnector;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GUI.Models
{
    class DatabaseConnection
    {
        private static (SshClient SshClient, uint Port) ConnectSsh(string sshHostName, string sshUserName,
         string sshPassword = null, string sshKeyFile = null, string sshPassPhrase = null, int sshPort = 22,
         string databaseServer = "192.168.68.108", int databasePort = 8455)
        {
            // check arguments
            if (string.IsNullOrEmpty(sshHostName))
                throw new ArgumentException($"{nameof(sshHostName)} must be specified.", nameof(sshHostName));
            if (string.IsNullOrEmpty(sshHostName))
                throw new ArgumentException($"{nameof(sshUserName)} must be specified.", nameof(sshUserName));
            if (string.IsNullOrEmpty(sshPassword) && string.IsNullOrEmpty(sshKeyFile))
                throw new ArgumentException($"One of {nameof(sshPassword)} and {nameof(sshKeyFile)} must be specified.");
            if (string.IsNullOrEmpty(databaseServer))
                throw new ArgumentException($"{nameof(databaseServer)} must be specified.", nameof(databaseServer));

            // define the authentication methods to use (in order)
            var authenticationMethods = new List<AuthenticationMethod>();

            if (!string.IsNullOrEmpty(sshKeyFile))
            {
                authenticationMethods.Add(new PrivateKeyAuthenticationMethod(sshUserName,
                    new PrivateKeyFile(sshKeyFile, string.IsNullOrEmpty(sshPassPhrase) ? null : sshPassPhrase)));
            }
            if (!string.IsNullOrEmpty(sshPassword))
            {
                authenticationMethods.Add(new PasswordAuthenticationMethod(sshUserName, sshPassword));
            }

            // connect to the SSH server
            var sshClient = new SshClient(new ConnectionInfo(sshHostName, sshPort, sshUserName, authenticationMethods.ToArray()));
            sshClient.Connect();

            // forward a local port to the database server and port, using the SSH server
            var forwardedPort = new ForwardedPortLocal("127.0.0.1", databaseServer, (uint)databasePort);
            sshClient.AddForwardedPort(forwardedPort);
            forwardedPort.Start();

            return (sshClient, forwardedPort.BoundPort);
        }

        //Select statement
        private static List<string> Select(string queryString, SshClient client, MySqlConnection connection)
        {
            string query = queryString;

            //Create a list to store the result
            List<string> list = new List<string>();

            //Open connection
            if (client.IsConnected)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                int fieldCOunt = dataReader.FieldCount;
                while (dataReader.Read())
                {
                    for (int i = 0; i < fieldCOunt; i++)
                    {
                        list.Add(dataReader.GetValue(i).ToString());
                    }
                }

                //close Data Reader
                dataReader.Close();

                //return list to be displayed
                return list;
            }
            return list;
        }

        public static Pair<List<string>, List<string>> Connect()
        {
            var server = "82.75.86.150";
            var sshUserName = "pi";
            var sshPassword = "SaxionTest";
            var databaseUserName = "arief";
            var databasePassword = "SaxionTest";

            var (sshClient, localPort) = ConnectSsh(server, sshUserName, sshPassword);

            Console.WriteLine($"\nHost: {sshClient.ConnectionInfo.Host}");
            Console.WriteLine($"Local port: {localPort.ToString()}");

            using (sshClient)
            {
                MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder
                {
                    Server = "127.0.0.1",
                    Port = localPort,
                    UserID = databaseUserName,
                    Password = databasePassword,
                    ConnectionTimeout = 30,
                };

                var connection = new MySqlConnection(csb.ConnectionString);
                connection.Open();

                Console.WriteLine($"\nDatabase connection status: {connection.State}");
                Console.WriteLine($"Database name: {connection}");
                Console.WriteLine($"Connection string: {connection.ConnectionString}\n");

                var temp_list = new List<string>();
                var limit = 0;
                var index = 42;

                // queries can be made here
                var py_query = "SELECT" + " * FROM PSET_test_db.PyData  ORDER BY idPyData desc LIMIT 43";
                var py_list = Select(py_query, sshClient, connection);

                for (int i = 0; i < 4; i++)
                {
                    // Check id
                    limit = int.Parse(py_list[index]) - 43;

                    Debug.WriteLine($"\nLimit is: {limit}");

                    py_query = $"SELECT" + $" * FROM PSET_test_db.PyData where idPyData < {limit} ORDER BY idPyData desc LIMIT 43";

                    temp_list = Select(py_query, sshClient, connection);

                    foreach (var element in temp_list)
                    {
                        py_list.Add(element);
                        index++;
                    }
                }

                var lht_query = "SELECT" + " * FROM PSET_test_db.LhtData  ORDER BY idLhtData desc LIMIT 40";
                var lht_list = Select(lht_query, sshClient, connection);

                // all_sensors(py_list, lht_list);
                var unparsed_sensors = new Pair<List<string>, List<string>>(py_list, lht_list);

                // return a list of types of sensors
                return unparsed_sensors;
            }
        }
    }
}
