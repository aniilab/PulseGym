import { Client } from "./client.model";
import { Trainer } from "./trainer.model";

export class WorkoutRequest {
  constructor(
    public Trainer: Trainer,
    public Client: Client,
    public Date: Date,
    public Status: string,
  ) {}
}

// export interface WorkoutRequest {
    // Trainer: Trainer;
    // Client: Client;
    // Date: Date;
    // Status: string;
// }