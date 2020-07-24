# Retrieving a bank account

{% api-method method="get" host="https://api" path="/bankaccounts/:id" %}
{% api-method-summary %}
bankaccounts
{% endapi-method-summary %}

{% api-method-description %}
This endpoint allows you to get all the bank accounts.
{% endapi-method-description %}

{% api-method-spec %}
{% api-method-request %}
{% api-method-path-parameters %}
{% api-method-parameter name="id" type="integer" required=true %}
ID of the bank account
{% endapi-method-parameter %}
{% endapi-method-path-parameters %}
{% endapi-method-request %}

{% api-method-response %}
{% api-method-response-example httpCode=200 %}
{% api-method-response-example-description %}
Bank account successfully retrieved.
{% endapi-method-response-example-description %}

```javascript
{
  "id": 1,
  "isActive": false,
  "customerName": "Dougal",
  "balance": 0.0
}
```
{% endapi-method-response-example %}

{% api-method-response-example httpCode=404 %}
{% api-method-response-example-description %}
Could not find a cake matching this query.
{% endapi-method-response-example-description %}

```javascript
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Not Found",
  "status": 404,
  "traceId": "0HM1FB0AFNVPM"
}
```
{% endapi-method-response-example %}
{% endapi-method-response %}
{% endapi-method-spec %}
{% endapi-method %}



