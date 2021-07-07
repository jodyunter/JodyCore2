interface ITeam {
  identifier: string;
  name: string;
  skill: number;
}

type TeamState = {
  teams: ITeam[]
}

type TeamAction = {
  type: string
  team: ITeam
}

type DispatchType = (args: TeamAction) => TeamAction

