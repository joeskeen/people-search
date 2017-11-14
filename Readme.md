# People Search

Need to search from a vast database of fake people? Have no fear, PeopleSearch is here! 

## Getting Started

In order to run this project, you will at least need Git and Visual Studio 2017 with Node.js tools installed on your machine.  As the database is hosted using Azure, you shouldn't need any other special database setup.  Just clone and run.

The PeopleSearch application is composed mainly of two views, the master view, which lists people and allows you to search, and the detail view, which displays specific information for an individual.

## Retrospective

### Some background on decisions made

The search functionality simply uses Entity Framework and Linq to build and execute the queries.  A good chunk of time was spent researching fulltext search capabilities in SQL Server, but after much experimentation I found it to perform just as well as the Linq solution, and as the Linq solution was simpler to understand and maintain, and was less prone to SQL injection attacks, I elected to stick with Linq.

I used ASP.NET CORE since that is the future of ASP.NET and Entity Framework, and because I haven't had an excuse to learn or use it yet and I wanted to get my feet wet using it.  Having come from earlier ASP.NET and EF versions, much was the same, but there were a few things that took some getting used to.

### Things that didn't make it

Realistically, it's impossible to completely exhaust the product backlog, and although I had delusions of grandeur regarding awesome features that could have made it into this app, I eventually had to resign to the reality that life is busy, and I had to work out a work-life balance that produced the best quality product without costing me my family.  Here's what didn't make it in the app:

* Import people from VCF files (exported from other sources).  I actually found a really great NuGet package to do this, and played around with it in LinqPad.  It wouldn't be too hard to implement, but didn't have time to put it in and make sure it was rock solid with the timeframe I had.  Also, with the database being hosted in the cloud, I didn't want anyone to put real-life people data into the app in case it was compromised (as I didn't have time for a full security review of the app).
* Export / Share a person as a VCF file.  This would have piggy-backed off of the previous feature, but in reverse.
* "New Person" workflow. Other than the security implications discussed above, I wanted to make this available only if it were rock-solid.  With many fields of varying types and lengths, I would have needed to export metadata about each field to ensure proper input validation before attempting to insert a new record into the database (as the DB error messages are often not super useful).  I recently wrote a set of controls that would use the proper constraints and validations based on the underlying SQL Server data type for the column; unfortunately these are closed-source (for work), written in jQuery (since they don't believe in anything newer than 10 years old), and was quite time-consuming to develop and test.  I didn't think I would have enough time to get that right for this project.
* "Edit Person" workflow.  Didn't make it for the same reasons stated above (for "New Person" workflow).
* "Delete Person" workflow.  Without the ability to create or modify people, it didn't make sense to allow deletion.
* Database normalization. The data in the database came from <fakenamegenerator.com>, which provided a SQL script for seeding the database.  I mostly used this generated table structure as-is (other than allowing `NULL`s in most of the columns and adding a Primary Key constraint) as it was pretty simple and allowed me to move forward with other parts of the app.  I do recognize, however, that there is some duplicated data (such as state, stateFull, country, countryFull) that could easily been moved out to its own table with foreign keys set up.  Also, I could have moved some of the optional data to a different table to keep the People table trim and focused.  For the scope of this application, however, I realized it would introduce a decent amount of complexity and reduce performance (when joining tables) where the primary use of the database is to query one table with very little variation.  So I decided to let it be.
* More testing. Honestly, there isn't a whole lot of logic in this app.  A lot of the code is just hooking up data to pass through from the database to the client.  I would, however, like to put some more test coverage on the PeopleController's search functionality.  The major hurdle for this was being able to inject mocks into the constructor of the PeopleController class.  In past versions of Entity Framework, `DbContext` and `DBSet` both implemented interfaces that you could easily create a mock from.  EF Core doesn't have a straightforward interface for doing these things, and I ended up having to pass concrete implementations of these classes into methods and constructors.  Yuck.  With more time I would have used a mocking framework like Moq to handle this for me.
  I would have also liked to get Karma/Jasmine(or Mocha) and Protractor working for the front-end testing.  I've done a lot with them in the past, but it does take a lot of time to set up and configure before you can start being productive, and I just ran out of time.
* SASSy CSS: I really like the things you can do with CSS preprocessors, such as SASS or LESS.  I tried hooking this into the Webpack process that comes with the ASP.NET Core WebApi / Angular template in VS with no success :(