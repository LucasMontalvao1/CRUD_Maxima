const apiUrl = 'https://localhost:7263';

export const environment = {
  production: false,
  apiUrl: apiUrl,
  endpoints: {
    login: `${apiUrl}/Api/login`,
    products: `${apiUrl}/Api/products`,
    departments: `${apiUrl}/Api/departments`,
  },
};