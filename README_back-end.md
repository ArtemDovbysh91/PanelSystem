# Back-end tasks
Please refactor and implement architecture and design patterns which make sense.

The existing `/survey/search` API endpoint needs to be modified to accept a search parameter which will be used to filter the results that contain a case-insensitive substring.

The results should also be sorted to display the surveys which will yield the best financial return for the time invested. This can be calculated as follows:

```
survey efficiency = incentive (euros) / length (minutes)
```

Finally, the results returned to the website front-end should also include the incentive (euros) and the length (minutes).

All existing unit tests should be updated to reflect the above requirements and new tests should be written to cover anything missing.

In addition, the two other API endpoints need to be implemented correctly:
1. GET `/survey/<id>`: returns the survey with the specific ID
1. POST `/survey`: creates a new survey with the parameters on the request body
