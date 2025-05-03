# CDC database using Debezium

![alt](./assets/architecture.png)

## Mô tả dự án

Dự án sử dụng debezium để sync các database với nhau, sử dụng docker compose để build dự án đơn giản hơn:

> - Debezium, Debezium connect
> - PostgreSQL
> - Kafka, kafka UI, Zookeper
> - Docker, docker-compose
> - .Net core console app for Consumer demo

### Setup kết nối debezium tới PostgreSQL

- Dùng postman để gọi tới: http://localhost:8083/connectors
- HTTP METHOD POST
- HEADER: Content-Type: application/json
- BODY:
  ```json
  {
    "name": "inventory-connector",
    "config": {
      "connector.class": "io.debezium.connector.postgresql.PostgresConnector",
      "database.hostname": "postgres",
      "database.port": "5432",
      "database.user": "postgres",
      "database.password": "postgres",
      "database.dbname": "inventory",
      "topic.prefix": "cdc-db",
      "table.include.list": "inventory.customers",
      "plugin.name": "pgoutput",
      "slot.name": "debezium_slot",
      "publication.name": "debezium_pub",
      "name": "inventory-connector"
    },
    "tasks": [],
    "type": "source"
  }
  ```

### PREVIEW Kết quả

```json
{
  "schema": {
    "type": "struct",
    "fields": [
      {
        "type": "struct",
        "fields": [
          {
            "type": "int32",
            "optional": false,
            "default": 0,
            "field": "id"
          },
          {
            "type": "string",
            "optional": false,
            "field": "first_name"
          },
          {
            "type": "string",
            "optional": false,
            "field": "last_name"
          },
          {
            "type": "string",
            "optional": false,
            "field": "email"
          }
        ],
        "optional": true,
        "name": "cdc-db.inventory.customers.Value",
        "field": "before"
      },
      {
        "type": "struct",
        "fields": [
          {
            "type": "int32",
            "optional": false,
            "default": 0,
            "field": "id"
          },
          {
            "type": "string",
            "optional": false,
            "field": "first_name"
          },
          {
            "type": "string",
            "optional": false,
            "field": "last_name"
          },
          {
            "type": "string",
            "optional": false,
            "field": "email"
          }
        ],
        "optional": true,
        "name": "cdc-db.inventory.customers.Value",
        "field": "after"
      },
      {
        "type": "struct",
        "fields": [
          {
            "type": "string",
            "optional": false,
            "field": "version"
          },
          {
            "type": "string",
            "optional": false,
            "field": "connector"
          },
          {
            "type": "string",
            "optional": false,
            "field": "name"
          },
          {
            "type": "int64",
            "optional": false,
            "field": "ts_ms"
          },
          {
            "type": "string",
            "optional": true,
            "name": "io.debezium.data.Enum",
            "version": 1,
            "parameters": {
              "allowed": "true,last,false,incremental"
            },
            "default": "false",
            "field": "snapshot"
          },
          {
            "type": "string",
            "optional": false,
            "field": "db"
          },
          {
            "type": "string",
            "optional": true,
            "field": "sequence"
          },
          {
            "type": "int64",
            "optional": true,
            "field": "ts_us"
          },
          {
            "type": "int64",
            "optional": true,
            "field": "ts_ns"
          },
          {
            "type": "string",
            "optional": false,
            "field": "schema"
          },
          {
            "type": "string",
            "optional": false,
            "field": "table"
          },
          {
            "type": "int64",
            "optional": true,
            "field": "txId"
          },
          {
            "type": "int64",
            "optional": true,
            "field": "lsn"
          },
          {
            "type": "int64",
            "optional": true,
            "field": "xmin"
          }
        ],
        "optional": false,
        "name": "io.debezium.connector.postgresql.Source",
        "field": "source"
      },
      {
        "type": "struct",
        "fields": [
          {
            "type": "string",
            "optional": false,
            "field": "id"
          },
          {
            "type": "int64",
            "optional": false,
            "field": "total_order"
          },
          {
            "type": "int64",
            "optional": false,
            "field": "data_collection_order"
          }
        ],
        "optional": true,
        "name": "event.block",
        "version": 1,
        "field": "transaction"
      },
      {
        "type": "string",
        "optional": false,
        "field": "op"
      },
      {
        "type": "int64",
        "optional": true,
        "field": "ts_ms"
      },
      {
        "type": "int64",
        "optional": true,
        "field": "ts_us"
      },
      {
        "type": "int64",
        "optional": true,
        "field": "ts_ns"
      }
    ],
    "optional": false,
    "name": "cdc-db.inventory.customers.Envelope",
    "version": 2
  },
  "payload": {
    "before": null,
    "after": {
      "id": 1001,
      "first_name": "Sally",
      "last_name": "Thomas",
      "email": "sally.thomas@acme.com"
    },
    "source": {
      "version": "2.7.3.Final",
      "connector": "postgresql",
      "name": "cdc-db",
      "ts_ms": 1746286340992,
      "snapshot": "first",
      "db": "inventory",
      "sequence": "[null,\"37461424\"]",
      "ts_us": 1746286340992882,
      "ts_ns": 1746286340992882000,
      "schema": "inventory",
      "table": "customers",
      "txId": 771,
      "lsn": 37461424,
      "xmin": null
    },
    "transaction": null,
    "op": "r",
    "ts_ms": 1746286341088,
    "ts_us": 1746286341088566,
    "ts_ns": 1746286341088566000
  }
}
```
