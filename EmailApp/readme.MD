# Sending Email Using Mailkit

## Add: MailKit

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