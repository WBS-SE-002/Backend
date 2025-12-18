import { Schema, model } from 'mongoose';

// create a schema for the message subdocument
const messageSchema = new Schema({
  role: {
    type: String,
    enum: ['assistant', 'developer', 'system', 'user'],
    required: true
  },
  content: { type: String, required: true }
});

const chatSchema = new Schema(
  {
    history: {
      type: [messageSchema],
      default: []
    }
  },
  { timestamps: true }
);

export default model('Chat', chatSchema);
