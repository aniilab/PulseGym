import { Component, Input } from '@angular/core';
import { Trainer } from 'src/app/models/trainer.model';

@Component({
  selector: 'app-trainer',
  templateUrl: './trainer.component.html',
  styleUrls: ['./trainer.component.css'],
})
export class TrainerComponent {
  @Input()
  public trainer: Trainer;
}
