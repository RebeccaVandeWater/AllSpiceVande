import { Profile } from "./Account.js"

export class Recipe {
  constructor(data) {
    this.id = data.id
    this.title = data.title
    this.instructions = data.instructions
    this.img = data.img
    this.category = data.category
    this.createdAt = new Date(data.createdAt)
    this.updatedAt = new Date(data.updatedAt)
    this.creatorId = data.creatorId
    this.creator = new Profile(data.creator)
  }
}