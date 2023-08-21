import { Profile } from "./Account.js"

export class Favorite {
  constructor(data) {
    this.id = data.id
    this.category = data.category
    this.createdAt = new Date(data.createdAt)
    this.updatedAt = new Date(data.updatedAt)
    this.creator = new Profile(data.creator)
    this.favoriteId = data.favoriteId
    this.img = data.img
    this.instructions = data.instructions
    this.title = data.title
  }
}