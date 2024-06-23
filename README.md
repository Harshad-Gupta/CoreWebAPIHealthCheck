ASP.NET Core WebAPI Project build in **.NET 8.0** with health parameters to get the **HEALTH** details of each controller's action method for different status code.
<br>
To get the details of API Health I have used concept of Middleware in .NET Core for **Logging** **Request** and **Response**.
<br><br>
To create health endpoint in my ASP.NET Core WebAPI project that provide details about the no of times specific endpoints are hit and the count of various status codes, you can follow the below steps:
<br>
<ol>
  <li>Create a Custom Middleware component for Logging Request and Response.</li>
  <li>Store the health data in Concurrent Dictionary.</li>
  <li>Create a new Health Controller endpoint to retrieve the data.</li>
</ol>
<h3>Example Reponse</h3>
<pre> 
{
  "Employee/AddNewEmployee": {
    "200": 1
  },
  "Health/GetHealthStatistics": {
    "200": 1
  },
  "Employee/GetEmployeeById": {
    "200": 1,
    "404": 1
  },
  "Employee/DeleteEmployeeById": {
    "200": 1
  },
  "Employee/GetAllEmployees": {
    "200": 1
  }
}
</pre>
