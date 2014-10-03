# Empty Realms #
This server will be open source but it will not be run by us (Open Realms), it's just a side project from BlackRayquaza (Lithium).
Feel free to use this source, although I (BlackRayquaza, Lithium) want credit for it.  

### How to set up the server ###
1. Make sure you have a (working) MySQL database.
2. Import the .SQL file from the "Db" folder.
3. Change the MySQL connection in "Db/Database.cs" to match with your MySQL database.
4. (Re)build the whole solution.
5. Go to the "Rabcdasm" folder and change the ip from the client (Rabcdasm/client-1/com/company/assembleegameclient/parameters/Parameters.class.asasm line 379).
6. Go back to the "Rabcdasm" folder and open "02 - Recompile Client.bat".
7. Open Server.exe and wServer.exe (Both are located in the "bin" folder).
8. Now open the client from the "Rabcdasm" folder.

