
# FoodStore .Net Core 8

- Developing a Robust and Maintainable .NET Core Web API following Clean Architecture: API, Core, and Infrastrucutre layers as mentioned in this [Link](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)

- The project is shows CRUD operations on 2 entities **categories** and **products** having 1 to many relation.




## Features

- Clean architecture
- Repository pattern
- API versioning
- Serilog
- CRUD operations on both entities
- Unit testing covering products service layer
- Integration testing using InMemory EF Db
- Pagination
- Filtering using LINQ Expressions on products
- .Net Core 8 (latest)
- Entity Framework


## Clean Architecture
Clean Architecture is a software design pattern aims to create systems that are easy to understand, maintain, and test by organizing code into distinct layers with clear responsibilities.

Architecture **3** main layers:

- **API Layer**: This layer serves as the entry point for external clients to interact with the application. It typically includes controllers, routes,filters, and middleware responsible for handling incoming HTTP requests and returning appropriate responses.

- **Core Layer**: The core layer contains the domain logic and business rules of the application. It encapsulates the essential functionality and represents the heart of the application's logic. This layer should be independent of any external frameworks or infrastructure concerns. It defines entities, data transfer object, repositories interfaces,services, and interfaces that model the business domain. The core layer should be highly testable (Unit testing) and contain the purest expression of the application's functionality.

- **Infrastructure Layer**: The infrastructure layer is responsible for providing implementations for external concerns such as databases, file systems, external services, logging, and caching. It contains concrete implementations of the repositories interfaces defined in the core layer, enabling the application to interact with external resources.

**Note :** Some developers opt to further divide the core layer into an "Application layer," particularly when employing patterns like MediatR/CQRS. However, in my project, I haven't introduced this layer as it's considered optional for the current scope of the application.

![MicrosoftCleanArchitecture](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/media/image5-7.png)

## Repository Pattern
The Repository Design Pattern that provides an abstraction layer between the application's business logic(Services) and the data access code (Entity framework in this repo), allowing for a separation of concerns and improved maintainability.
## Installation

Clone GitHub Repository 

```bash
  gh repo clone kirolos997/FoodStoreMF

  cd FoodStoreMF
```

Change database connection string by your connection for the given key in **appsettings.json**
```
LocalHostDb
```

Apply database migrations
```bash
  cd /FoodStoreMF/FoodStore.Infrastrucutre/Migrations

  PM> Update-Database Initial

  PM> Update-Database SetNullDeleteBehavior
```
Make sure to select **HTTP profile** before running the application on port **5089**.
## API Reference for products entity

#### Get all Products

```http
  GET /api/v1/products{?offset}&{?limit}{?search}
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `?offset` | `int`    | **Optional** Number of items to skip|
| `?limit` | `int`     | **Optional** Number of items to take|
| `?search` | `string` | **Optional** multiple or one filter(s) anded together written in the following format ['ColumName' 'Operator[eq, neq, lt, gt 'Value' ]. Example: ?search=productName eq P1& search=price neq 10 |

#### Get Product

```http
  GET /api/v1/products/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `Guid` | **Required**. Id of product to fetch |

#### Delete Product

```http
  DELETE /api/v1/products${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `Guid` | **Required**. Id of product to delete |

#### Update Product

```http
  PUT /api/v1/products${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `Guid` | **Required**. Id of product to update |

| JSON Body | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `InStore`      | `Bool`  | **Required**. Product exists inside store or no |
| `Price`      | `decimal(2 digits after dot)`  | **Required**. Product price |
| `ProductName`      | `string`  | **Required**. Product name |
| `ProductDescription`      | `string`  | **Required**. Product description|
| `CategoryId`      | `Guid`  | **Required**. Category ID for the product and must be a valid category|

#### Create Product

```http
  POST /api/v1/products
```

| JSON Body | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `InStore`      | `Bool`  | **Required**. Product exists inside store or no |
| `Price`      | `decimal(2 digits after dot)`  | **Required**. Product price |
| `ProductName`      | `string`  | **Required**. Product name and must be not **duplicated** before |
| `ProductDescription`      | `string`  | **Required**. Product description|
| `CategoryId`      | `Guid`  | **Required**. Category ID for the product and must be a valid category|


## API Reference for Categories entity

#### Get all Categories without linked products and no filter (v1)

```http
  GET /api/v1/Categories{?offset}&{?limit}
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `?offset` | `int`    | **Optional** Number of items to skip|
| `?limit` | `int`     | **Optional** Number of items to take|

#### Get all Categories with linked products and filter (v2)

```http
  GET /api/v2/Categories{?offset}&{?limit}{?search}
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `?offset` | `int`    | **Optional** Number of items to skip|
| `?limit` | `int`     | **Optional** Number of items to take|
| `?search` | `string` | **Optional** filtewritten in the following format [ name 'Operator[eq, neq, lt, gt 'Value' ]. Example: ?search=**name** eq P1 |

#### Get Category without linked products(v1)

```http
  GET /api/v1/Categories/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `Guid` | **Required**. Id of category to fetch |


#### Get Category with linked products(v2)

```http
  GET /api/v2/Categories/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `Guid` | **Required**. Id of category to fetch |



#### Delete Category

```http
  DELETE /api/v1/Categories${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `Guid` | **Required**. Id of category to delete. **Note** if category is deleted the products linked won't be deleted |

#### Update Category

```http
  PUT /api/v1/Categories${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `Guid` | **Required**. Id of category to update |

| JSON Body | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `CategoryName`   | `string`  | **Required**. Category name|

#### Create Category

```http
  POST /api/v1/Categories
```

| JSON Body | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `CategoryName`   | `string`  | **Required**. Category name and must not be **duplicated** before|
