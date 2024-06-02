import http from "k6/http";
import { randomString } from "https://jslib.k6.io/k6-utils/1.2.0/index.js";
import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";

// Define the load test configuration
export const options = {
  summaryTrendStats: [
    "avg", //average
    "min",
    "med", //median
    "max",
    "p(50)",
    "p(90)",
    "p(95)",
    "p(99)",
    "p(99.99)",
    "count",
  ],
  duration: '20m',
  summaryTimeUnit: "ms",
  iterations: 3000
};

const query = `
mutation ($input: CreateCustomerRequestDtoInput!) {
  addCustomer(createCustomerRequestDto: $input) {
    name
    email
    username
  }
}
`;

export default function () {
  const headers = {
    "Content-Type": "application/json",
  };

  let value = randomString(20);
  const res = http.post(
    "http://localhost:5000/graphql",
    JSON.stringify({
      query: query,
      variables: {
        input: {
          name: value,
          password: value,
          email: value,
          username: value,
        },
      },
    }),
    {
      headers: headers,
    }
  );
}

export function handleSummary(data) {
  return {
    "graphqlCreateUser.html": htmlReport(data),
  };
}
