# Mail-Service-Using-Windows-Service

The project consists of a windows service that is prior to checks a particular folder in any given specific time (15 minutes is taken as default) for XML files. It is then used to validate that they have the required following format:
<EmailMessage>
<To></To>
<Subject></Subject>
<MessageBody></MessageBody>
</EmailMessage>
and generates an email to send it to the specific reciepents. 
