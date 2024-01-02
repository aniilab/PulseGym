import { Component, Input } from '@angular/core';
import { Program } from 'src/app/models/program.model';
import { ProgramViewDTO } from 'src/app/models/program/program-view-dto';

@Component({
  selector: 'app-program',
  templateUrl: './program.component.html',
  styleUrls: ['./program.component.css']
})
export class ProgramComponent {
  @Input() 
  public program: ProgramViewDTO;
}

