﻿using DatabaseClassifications;

public class QuizSession {
	public bool isChallengingUser;
	public Category category;
	public Quiz quiz;

	public User defender;

	public QuizSession(bool isChallengingUser){
		this.isChallengingUser = isChallengingUser;
	}
}