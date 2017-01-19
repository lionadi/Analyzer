Project World Analysis

Contents
Installation	1
MongoDB	1
Set up the MongoDB environment.	1
Start MongoDB.	2
Create a configuration file.	3
Functionalities	3
Visual Studio Projects	4
Analyzer.Common	4
Analyzer.WebCrawler	5
AnalyzerConfiguratorApplication	5
AnalyzerServiceApplication	6
WebCrawlerTester	6
Solution Architecture	6
C# Web Scraper	6
Database	7
Web scraping	7
Ways to respect the site and its users when scraping to void causing harm to the user experience or the service	7
Logging	7
Configurations	7


Installation

MongoDB
Set up the MongoDB environment.
MongoDB requires a data directory to store all data. MongoDB’s default data directory path is the absolute path \data\db on the drive from which you start MongoDB. Create this folder by running the following command in a Command Prompt:
md \data\db
You can specify an alternate path for data files using the --dbpath option to mongod.exe, for example:
"C:\Program Files\MongoDB\Server\3.4\bin\mongod.exe" --dbpath d:\test\mongodb\data
If your path includes spaces, enclose the entire path in double quotes, for example:
"C:\Program Files\MongoDB\Server\3.4\bin\mongod.exe" --dbpath "d:\test\mongo db data"
Start mongo using a configuration files that specifies how MongoDB works::
"C:\Program Files\MongoDB\Server\3.4\bin\mongod.exe" --config "C:\Program Files\MongoDB\Server\3.4\mongod.cfg"

You may also specify the dbpath in a configuration file.
Later, to stop MongoDB, press Control+C in the terminal where the mongod instance is running
Start MongoDB.
To start MongoDB, run mongod.exe. For example, from the Command Prompt:
"C:\Program Files\MongoDB\Server\3.4\bin\mongod.exe"
This starts the main MongoDB database process. The waiting for connections message in the console output indicates that the mongod.exe process is running successfully.
Depending on the security level of your system, Windows may pop up a Security Alert dialog box about blocking “some features” of C:\Program Files\MongoDB\Server\3.4\bin\mongod.exe from communicating on networks. All users should select Private Networks, such as my home or work network and click Allow access. For additional information on security and MongoDB, please see the Security Documentation.
Create a configuration file.
Create a configuration file. The file must set systemLog.path. Include additional configuration options as appropriate.
For example, create a file at C:\Program Files\MongoDB\Server\3.4\mongod.cfg that specifies both systemLog.path and storage.dbPath:
systemLog:
    destination: file
    path: c:\data\log\mongod.log
storage:
    dbPath: c:\data\db



More info on the MongoDB site: https://docs.mongodb.com/manual/tutorial/install-mongodb-on-windows/

Functionalities

•	A web site to display results
•	Data analysis on web content to find out things(these should be automated):
o	Categorization and analysis of news items (to be able to show country, regions in a country, and to sort by date, by profession, income etc)
	Categories
•	Science
•	Sports
•	Politics
•	Economics
•	etc
	Find correlations between news items, between the news items and countries, region within them
	Categorize what kind of content, phrasing, keywords etc are used
	Are the news items positive, neutral, negative
	Each of these results should be displayed geographically, to be able to be chose a time period
•	Using docker + kubernetes to handle distributed load balancing and web processing
•	Using AWS, Google Cloud and /or Azure to host these things.

Visual Studio Projects

Analyzer.Common

Contains all the common functionality.
•	Database operations
•	Serializations
•	IO Functionalities
•	Logging
•	Constants
•	Extensions
 
Analyzer.WebCrawler
Contains all the logic for scraping the web
 
AnalyzerConfiguratorApplication

Is an application that simply allows you to edit sources to be processed from the web.
 
 
AnalyzerServiceApplication
This is the main application that is working as a “service”. You can start and stop the web scraping and observe minimalistic what is happening.
 
 
WebCrawlerTester

A simple Windows Application to test web scraping algorithms.

Solution Architecture

C# Web Scraper

Database
Writers and updates
Database writes are performed in a queue manner. There is a main DB operator which receives data to be inserted to the DB. The operator itself handles all the DB logic: how many items are processed at a time. 
When the time comes to write to the database the queue is copied into another collection from which the writes are performed. This way the application can continue to write to the queue while the writes to the database take place.
The collections are written “on the go” based on the given parameters to the write operation. If the desired collection does not exist then it is created.

Web scraping
This will be a multithreaded operation that reads web URLs, scrapers the data and passes it to the DB operator.
The application keeps track of how many items it has processed. You can start and stop the scraping. For each scraping source, there is a time stamp when the source was last time processed. This functionality is so that the same data is not read twice into the database.
Web Pages scraping work in such a way that preferably there is a RSS feed that is read. From the feed items are retrieved based on how many days old they are. If they are new enough they are processed.
Web sources are edited with a Windows application that saves to the HDD the configurations to be read by the web scraper “service”.
Ways to respect the site and its users when scraping to void causing harm to the user experience or the service
For this solution, the code uses these methods:
•	Pausing between requests of content
•	Making a greater list of sources and shuffling them. This way the same site is not necessarily one after the other
•	Using compression automatically if available to minimize traffic
•	The ability to time how often sites are scraped, once a day, once a week, month etc
•	The ability to read content from RSS Feeds and also to be able to define time ranges
•	The randomly define scraping intervals with the following equation: min interval + random number of the interval value between 0 and max interval value.

Logging
All “main” functionalities and operations are logged into the MongoDB database in a single collection.

Configurations
