import { Component, Input } from '@angular/core';
import { Program } from 'src/app/models/program.model';

@Component({
  selector: 'app-program',
  templateUrl: './program.component.html',
  styleUrls: ['./program.component.css']
})
export class ProgramComponent {
  @Input() 
  public program: Program;
}

