export class Recipe {
  constructor(data) {
    this.id = data.id
    this.createdAt = new Date(data.createdAt)
    this.updatedAt = new Date(data.updatedAt)
    this.title = data.title
    this.category = data.category
    this.instructions = data.instructions
    this.img = data.img
    this.creatorId = data.creatorId
    this.creator = data.creator
  }
}

// let creator =
//   "creator": {
//     "id": "64adbe67ba9f527398ff6ae2",
//     "createdAt": "2023-08-16T21:07:03",
//     "updatedAt": "2023-08-16T21:07:03",
//     "name": "fakeperson@fakeemail.com",
//     "picture": "https://s.gravatar.com/avatar/f224f703762042006385385767a13582?s=480&r=pg&d=https%3A%2F%2Fcdn.auth0.com%2Favatars%2Ffa.png"
//   }
