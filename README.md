Security project 1

Description:
This assignment will focus on the existence of vulnerabilities in software projects, their exploration and avoidance. The objective is for students to develop a small application, with a simple and clear purpose. An online shop, a forum, a wiki, or a RESTFull service are good examples of what is expected. The application should provide its function without errors, without inconsistent behaviour, and without pages/sections/fragments that do not fit the purpose of the application.

However, this application should also suffer from a specific set of weaknesses, which are not obvious to the casual user, but may be used to compromise the application, or the system.

Students should provide a both a flawed and correct version of the application, together with a report demonstrating how those vulnerabilities are explored and their impact. The project must include vulnerabilities associated with CWE-79 and CWE-89. An additional set of weaknesses must be considered, so that the total number of vulnerabilities should be of at least 4

How to run:

backend:
1. open to-do-backend folder with the terminal
2. if not installed, run: dotnet tool install --global dotnet-ef
3. dotnet-ef restore
4. dotnet-ef database update
5. Start the application with Visual studio

frontend:
1. open to-do-frontend folder with the terminal.
2. npm install
3. npm start
4. Application can be reached at localhost:3000