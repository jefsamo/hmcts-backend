# Task API

Base URL: `https://localhost:5001` (or `http://localhost:5000`)

## Create Task

**Endpoint**

`POST /api/tasks`

**Description**

Creates a new task in the system and returns the created task.

**Request Body (application/json)**

```json
{
  "title": "My task title",
  "description": "Optional description",
  "status": "Todo",
  "dueDateTime": "2025-11-30T10:00:00Z"
}
