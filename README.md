
<br />
<p align="center">

  <h3 align="center">Ecommerce Checkout</h3>

  <p align="center">
    Build using ASP.NET Core 5.0 (WebApi & MVC)
    <br />
  </p>
</p>

<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#features-included">Features Included</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#support">Support</a></li>
  </ol>
</details>

## About The Project

Clean Architecture Solution Template for ASP.NET Core 5.0. Built for exercise of checkout system for a shop which sell only apples and oranges. Build a system which take list of items as string input
Then apply discount offers like 
1) buy one , get one free
2) 3 for price of 2

### Built with

-   [ASP.NET Core 5.0 MVC](https://dotnet.microsoft.com/learn/aspnet/what-is-aspnet-core)
-   ASP.NET Core 5.0 WebAPI
-   [Entity Framework Core 5.0](https://docs.microsoft.com/en-us/ef/core/)

## Getting Started

An idea to bring together the best and essential practices / packages of ASP.NET Core 5.0 along best solution for product checkout

### Prerequisites

-   Make sure you are running on the latest .NET 5 SDK (SDK 5.0 and above only). [Get the latest one here.](https://dotnet.microsoft.com/download/dotnet/5.0)

-   Visual Studio 2019 (v16.8+) (You can check out my Installation Guide of [Visual Studio 2019 Community](https://code.com/blog/install-visual-studio-2019-community/) which is completely Free to use.) Make sure that ASP.NET and web development workload is installed.

-   Install the latest [.NET & EF CLI Tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet) by using this command :

    ```.NET Core CLI
    dotnet tool install --global dotnet-ef
    ```
## Steps to Run

1) Database readiness
The database migration has already been created, all is require is to update database connection in appsetting and
in Package Manager Console run following commands

```
update-database -context ApplicationDbContext
update-database -context IdentityContext
```

After this you can verify the in your database the tables like Product, Discount would get created with data in them.

2) Code readiness
Mark the CheckoutSys.Api as startup project and run it
It will open the swagger page where multiple API endpoints available like add/delete/update for product and discount plus the checkout api to get the order total
Use version = 1 in all endpoints
In checkout API put below as the body
```
{
  "products": [
    "Apple","Apple","Orange","Apple"
  ],
  "applyDiscount": true
}
```
Pass property applyDiscount:true when you want to apply discount else false

We have prices apple at 60 paise and orange at 25 paise and 2 discount already created 
o  buy one, get one free on Apples
o 3 for the price of 2 on Oranges

Output 
```
{
  "data": {
    "orderTotal": 1.45,
    "orderDiscount": 0.6
  },
  "failed": false,
  "message": null,
  "succeeded": true
}
```

## Contributing

Contributions are what make the open-source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request


## Support

Has this Project helped you learn something New? or Helped you at work? Do Consider Supporting. Here are a few ways by which you can support.

-   Leave a star! ⭐️
-   Recommend this awesome project to your colleagues.




