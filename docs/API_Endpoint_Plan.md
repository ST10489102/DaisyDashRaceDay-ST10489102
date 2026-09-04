# DaisyDash - API Endpoint Plan

## 1. Users
- POST /api/users/register
- POST /api/users/login
- GET /api/users/{id}

## 2. Events
- POST /api/events
- GET /api/events
- GET /api/events/{id}
- PUT /api/events/{id}
- DELETE /api/events/{id}

## 3. Enrolments
- POST /api/enrolments
- GET /api/enrolments/user/{userId}
- GET /api/enrolments/event/{eventId}

## 4. Payments
- POST /api/payments
- GET /api/payments/{enrolmentId}

## 5. Results
- POST /api/results
- GET /api/results/event/{eventId}
