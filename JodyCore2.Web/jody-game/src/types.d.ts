interface ITeam {
  identifier: string;
  name: string;
  skill: number;
}

type TeamState = {
  loaded: boolean
  teams: ITeam[]
}

type TeamAction = {
  type: string
  team: ITeam
  teams: ITeam[]
}


type DispatchType = (args: TeamAction) => TeamAction

