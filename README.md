# Appointment API: 

Microservices on .NET core and AWS cloud platform which uses ASP.NET Web API, Lambda, API Gateway, DynamoDB, and Clean Code Architecture. 
Also includes cloud formation, SAM, Swagger, xUnit, Moq, and Automapper.

![alt text](https://github.com/CyborgHead/appointment/blob/main/architecture.png?raw=true)



##### Swagger UI: 

https://cvrqmbit5d.execute-api.eu-west-1.amazonaws.com/swagger/index.html 




###### Configurations: 

Custom policy to read/write specific dynamo db table must be attached to created Lambda role.


**dynamodb-appointment-state-store-crud-policy**

```
{
    "Version": "2012-10-17",
    "Statement": [
        {
            "Sid": "DynamoDBIndexAndStreamAccess",
            "Effect": "Allow",
            "Action": [
                "dynamodb:GetShardIterator",
                "dynamodb:Scan",
                "dynamodb:Query",
                "dynamodb:DescribeStream",
                "dynamodb:GetRecords",
                "dynamodb:ListStreams"
            ],
            "Resource": [
                "arn:aws:dynamodb:{replace_with_region}:{replace_with_aws_account_id}:table/appointment-state-store/*",
                "arn:aws:dynamodb:{replace_with_region}:{replace_with_aws_account_id}:table/appointment-state-store/stream/*"
            ]
        },
        {
            "Sid": "DynamoDBTableAccess",
            "Effect": "Allow",
            "Action": [
                "dynamodb:BatchGetItem",
                "dynamodb:BatchWriteItem",
                "dynamodb:ConditionCheckItem",
                "dynamodb:PutItem",
                "dynamodb:DescribeTable",
                "dynamodb:DeleteItem",
                "dynamodb:GetItem",
                "dynamodb:Scan",
                "dynamodb:Query",
                "dynamodb:UpdateItem"
            ],
            "Resource": "arn:aws:dynamodb:{replace_with_region}:{replace_with_aws_account_id}:table/appointment-state-store"
        },
        {
            "Sid": "DynamoDBDescribeLimitsAccess",
            "Effect": "Allow",
            "Action": "dynamodb:DescribeLimits",
            "Resource": [
                "arn:aws:dynamodb:{replace_with_region}:{replace_with_aws_account_id}:table/appointment-state-store",
                "arn:aws:dynamodb:{replace_with_region}:{replace_with_aws_account_id}:table/appointment-state-store/index/*"
            ]
        }
    ]
}
```
