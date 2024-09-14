const apiUrl = 'https://localhost:7263';

export const environment = {

  production: false,
  apiUrl: apiUrl,
  loginEndpoint: `${apiUrl}/Api/v1/login`,
  productsEndpoint: `${apiUrl}/Api/products`,
  departmentsEndpoint: `${apiUrl}/Api/departments`,

};