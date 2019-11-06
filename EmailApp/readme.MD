# Sending Email Using Mailkit

* https://www.excitoninteractive.com/articles/read/69/asp-net-core2/sending-email-using-mailkit

## Step #4: Add: MailKit

* MailKit is an Open Source cross-platform .NET mail-client library that is based on MimeKit and optimized for mobile devices.

Features include:
* HTTP, Socks4, Socks4a and Socks5 proxy support.
* SASL Authentication via SCRAM-SHA-256, SCRAM-SHA-1, NTLM, DIGEST-MD5, CRAM-MD5, LOGIN, PLAIN, and XOAUTH2.
* A fully-cancellable SmtpClient with support for STARTTLS, 8BITMIME, BINARYMIME, ENHANCEDSTATUSCODES, SIZE, DSN, PIPELINING and SMTPUTF8.
* A fully-cancellable Pop3Client with support for STLS, UIDL, APOP, PIPELINING, UTF8, and LANG.
* A fully-cancellable ImapClient with support for ACL, QUOTA, LITERAL+, IDLE, NAMESPACE, ID, CHILDREN, LOGINDISABLED, STARTTLS, MULTIAPPEND, UNSELECT, UIDPLUS, CONDSTORE, ESEARCH, SASL-IR, COMPRESS, WITHIN, ENABLE, QRESYNC, SORT, THREAD, LIST-EXTENDED, ESORT, METADATA / METADATA-SERVER, NOTIFY, FILTERS, LIST-STATUS, SORT=DISPLAY, SPECIAL-USE / CREATE-SPECIAL-USE, SEARCH=FUZZY, MOVE, UTF8=ACCEPT / UTF8=ONLY, LITERAL-, APPENDLIMIT, STATUS=SIZE, OBJECTID, XLIST, and X-GM-EXT1.
* Client-side sorting and threading of messages (the Ordinal Subject and the Jamie Zawinski threading algorithms are supported).
* Asynchronous versions of all methods that hit the network.
* S/MIME, OpenPGP, DKIM and ARC support via MimeKit.
* Microsoft TNEF support via MimeKit.

## Add: File Features.Messaging.Attachment.cs

*  Our attachment class will take care of holding the data that we want to attach to our email as well as convert it to a memory stream that will allow us to actually attach it to our email. 

## Add: File Features.Messaging.MessageService.cs

* The interface for our message service that specifies the fundamental method of sending an email. 
* Our message service class that will be responsible for taking in the required information and sending out a corresponding email. 

## Set up: dependency injection

*  Configuring our concrete message service to be injected whenever we ask for a class that implements the interface. 

## Step #5: Add: Emailing Exceptions

* In this article we will create both a template and model that we can use to generate an email from an exception. With that in place we will then create a middleware that we can use to send an email automatically if our application encounters an unhandled exception. And of course to do this the last step will be to add our new middleware to the pipeline within our Startup class.

## Correct: location of the exception email

* Correcting the error of specifying the location of the exception email location. 

```
...
namespace WebUi.Features.Messaging
{
    public class MessageService : IMessageService
    {
        ...
        public async Task SendExceptionEmailAsync(Exception e, HttpContext context)
        {
            var message = _viewRenderer.Render("Features/Messaging/Email/ExceptionEmail", new ExceptionEmailModel(e, context));
            ...
        }
    }
}
```

## Add: Email files

*  The model that we will use to hold the information that we want to include in our email. 
*  The code that will be used to generate the actual body of our exception email. 

## Update: our message service

```
namespace WebUi.Features.Messaging
{
    public interface IMessageService
    {
        ...
        Task SendEmailToSupportAsync(string subject, string message);
        Task SendExceptionEmailAsync(Exception e, HttpContext context);
    }
}
```
* Our message service now makes it easier to send emails to our support address as well as handling sending emails for any exceptions that we pass it. 

## Add: Notification of any exceptions

* The exception logging middleware will take care of automatically sending an email whenever an exception is thrown. 

## Add: The middleware to the pipeline

* Adding the exception logging middleware to the pipeline. 

## Step #3: View Location Expander and View Renderer 

* In this article we will expand the locations that the razor view engine will search to find razor views and we will create a razor view renderer that we can inject into our controllers and pages. We are doing this for a few different reasons but the two we will be exploring in just a little while is sending emails and indexing the content of our application for full text searching.

## Add: Look for razor files outside of the views folder

* ViewLocationExpander
* Creating a view expander so that we are able to specify and arbitrary location for our razor files. 

## Add: Register our expander

```
...
using WebUi.Infrastructure;

namespace WebUi
{
    public class Startup
    {
        ...
        public void ConfigureServices(IServiceCollection services)
        {
            ...
            services.Configure<RazorViewEngineOptions>(x => x.ViewLocationExpanders.Add(new ViewLocationExpander()));
        }
        ...
    }
}
```
## Add: An injectable view renderer

* The interface that we will implement for our view renderer. 
*  Our view renderer that we will be able to inject into our controllers and pages when needed. 

## Add: a service

```
...
using WebUi.Infrastructure;

namespace WebUi
{
    public class Startup
    {
        ...
        public void ConfigureServices(IServiceCollection services)
        {
            ...
            services.AddTransient<IViewRenderer, ViewRenderer>();
        }
        ...
    }
}
```
## Test Run: Test.cshtml

```
@model string

<h1>@Model</h1>

<p>This is a test</p>
```
*  Our test razor page which simply takes in a string as the model.
